using Stateless;
using System;
using System.Threading;

namespace AlarmClockStateMachine
{
    public class AlarmClockStateMachine
    {
        public enum AlarmClockStates
        {
            AlarmIdle,
            AlarmSet,
            AlarmRinging,
        }

        public enum AlarmClockTriggers
        {
            DeactivateAlarm,
            SetAlarm,
            ActivateAlarm
        }

        public AlarmClockStates State { get; private set; }
        private StateMachine<AlarmClockStates, AlarmClockTriggers> _machine;
        private DateTime _alarmTime;

        public AlarmClockStateMachine(DateTime alarmTime)
        {
            _alarmTime = alarmTime;
            State = AlarmClockStates.AlarmSet;

            _machine = new StateMachine<AlarmClockStates, AlarmClockTriggers>(() => State, s => State = s);

            _machine.Configure(AlarmClockStates.AlarmIdle)
                .Permit(AlarmClockTriggers.SetAlarm, AlarmClockStates.AlarmSet);

            _machine.Configure(AlarmClockStates.AlarmSet)
                 .Permit(AlarmClockTriggers.ActivateAlarm, AlarmClockStates.AlarmRinging);

            _machine.Configure(AlarmClockStates.AlarmRinging)
                .Permit(AlarmClockTriggers.DeactivateAlarm, AlarmClockStates.AlarmIdle);

            while(true)
            {
                if(DateTime.Now >= _alarmTime)
                {
                    _machine.Fire(AlarmClockTriggers.ActivateAlarm);
                    break;
                }
            }
        }

        public void DeactivateAlarm()
        {
            _machine.Fire(AlarmClockTriggers.DeactivateAlarm);
        }

        public void ResetAlarm(DateTime alarmTime)
        {
            _machine.Fire(AlarmClockTriggers.SetAlarm);
            _alarmTime = alarmTime;
        }
            
    }
}
