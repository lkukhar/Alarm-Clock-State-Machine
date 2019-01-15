using System;
using System.Threading;

namespace AlarmClockStateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            AlarmClockStateMachine acsm = new AlarmClockStateMachine(DateTime.Now.AddMinutes(1));
            Console.WriteLine("Current State: " + acsm.State); //Should Be AlarmRinging
            acsm.DeactivateAlarm();
            Console.WriteLine("Current State: " + acsm.State); //Should Be AlarmIdle
            acsm.ResetAlarm(DateTime.Now.AddMinutes(1));
            Console.WriteLine("Current State: " + acsm.State); //Should Be AlarmSet
            Console.ReadLine();
        }
    }
}
