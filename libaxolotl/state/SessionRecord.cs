using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static libaxolotl.state.StorageProtos;

namespace libaxolotl.state
{
    /**
 * A SessionRecord encapsulates the state of an ongoing session.
 *
 * @author Moxie Marlinspike
 */
    public class SessionRecord
    {

        private static int ARCHIVED_STATES_MAX_LENGTH = 40;

        private SessionState sessionState = new SessionState();
        private LinkedList<SessionState> previousStates = new LinkedList<SessionState>();
        private bool fresh = false;

        public SessionRecord()
        {
            this.fresh = true;
        }

        public SessionRecord(SessionState sessionState)
        {
            this.sessionState = sessionState;
            this.fresh = false;
        }

        public SessionRecord(byte[] serialized)
        {
            RecordStructure record = RecordStructure.ParseFrom(serialized);
            this.sessionState = new SessionState(record.CurrentSession);
            this.fresh = false;

            foreach (SessionStructure previousStructure in record.PreviousSessionsList)
            {
                previousStates.AddLast(new SessionState(previousStructure)); // add -> AddLast (java)
            }
        }

        public bool hasSessionState(uint version, byte[] aliceBaseKey)
        {
            if (sessionState.getSessionVersion() == version &&
                Enumerable.SequenceEqual(aliceBaseKey, sessionState.getAliceBaseKey()))
            {
                return true;
            }

            foreach (SessionState state in previousStates)
            {
                if (state.getSessionVersion() == version &&
                    Enumerable.SequenceEqual(aliceBaseKey, state.getAliceBaseKey()))
                {
                    return true;
                }
            }

            return false;
        }

        public SessionState getSessionState()
        {
            return sessionState;
        }

        /**
         * @return the list of all currently maintained "previous" session states.
         */
        public LinkedList<SessionState> getPreviousSessionStates()
        {
            return previousStates;
        }


        public bool isFresh()
        {
            return fresh;
        }

        /**
         * Move the current {@link SessionState} into the list of "previous" session states,
         * and replace the current {@link org.whispersystems.libaxolotl.state.SessionState}
         * with a fresh reset instance.
         */
        public void archiveCurrentState()
        {
            promoteState(new SessionState());
        }

        public void promoteState(SessionState promotedState)
        {
            this.previousStates.AddFirst(sessionState);
            this.sessionState = promotedState;

            if (previousStates.Count > ARCHIVED_STATES_MAX_LENGTH)
            {
                previousStates.RemoveLast();
            }
        }

        public void setState(SessionState sessionState)
        {
            this.sessionState = sessionState;
        }

        /**
         * @return a serialized version of the current SessionRecord.
         */
        public byte[] serialize()
        {
            List<SessionStructure> previousStructures = new List<SessionStructure>();

            foreach (SessionState previousState in previousStates)
            {
                previousStructures.Add(previousState.getStructure());
            }

            RecordStructure record = RecordStructure.CreateBuilder()
                                                    .SetCurrentSession(sessionState.getStructure())
                                                    .AddRangePreviousSessions(previousStructures)
                                                    /*.AddAllPreviousSessions(previousStructures)*/
                                                    .Build();

            return record.ToByteArray();
        }

    }
}
