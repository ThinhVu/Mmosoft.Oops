using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Mmosoft.Oops.Animation
{
    public class Animator
    {
#if DEBUG
        public string Name { get; set; }
#endif
        private List<System.Windows.Forms.Timer> timers;
        private State state;

        public Action OnCompleted { get; set; }
        public Action OnStopped { get; set; }
        public bool Loop { get; set; }

        public Animator()
        {
            timers = new List<System.Windows.Forms.Timer>();
            state = State.Idle;
        }

        //        
        public void Add(Step detail)
        {            
            // init current timer
            var timer = new System.Windows.Forms.Timer { Interval = detail.Interval };
            // index of current timer
            var currentTimerIndex = timers.Count;
            
            // timer tick stuff
            // currentStep and maximumStep is closure variable
            int currentStep = 0;
            int maximumStep = detail.TotalStep;
            timer.Tick += (s, e) => 
            {
                if (state == State.StopRequested)
                {
#if DEBUG
                    Debug.Print(Name + " animation stopped!");
#endif
                    if (OnStopped != null)
                        OnStopped.Invoke();
                    currentStep = 0;
                    timer.Stop();
                    state = State.Idle;
                }
                else
                {
                    // 
                    currentStep++;

                    if (currentStep <= maximumStep)
                    {
                        if (detail.AnimAction != null)
                            detail.AnimAction.Invoke(currentStep);                        
                    }
                    else
                    {
                        // resset current step to re-run current timer
                        currentStep = 0;
                        timer.Stop();

                        // run a timer after current timer if it exists
                        if (timers.Count > currentTimerIndex + 1)
                        {
                            timers[currentTimerIndex + 1].Start();
                        }
                        else if (Loop)
                        {
                            timers[0].Start();
                        }
                        else
                        {
#if DEBUG
                            Debug.Print(Name + " animation completed!");
#endif
                            state = State.Idle;
                            if (OnCompleted != null)
                                OnCompleted.Invoke();
                        }
                    }
                }               
            };
            timers.Add(timer);
        }
        public void Remove(Step detail)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            if (state == State.Started)
            {
                Stop();
                while (state != State.Idle) { Thread.Sleep(100); };
                foreach (var timer in timers)
                    timer.Dispose();
                timers = new List<System.Windows.Forms.Timer>();
            }
        }
        public void Wait(int milisecond)
        {
            Add(new Step
            {
                Interval = milisecond,
                TotalStep = 1
            });
        }
        //
        public void Start()
        {
#if DEBUG
            Debug.Print("Trying to start " + Name + " with state :" + state);
#endif
            if (state == State.Idle)
            {
                if (timers.Count > 0)
                {
                    state = State.Started;
                    timers[0].Start();
                }
            }
        }
        public void Stop()
        {
#if DEBUG
            Debug.Print("Trying to stop :" + Name + " with state: " + state);
#endif
            if (state == State.Started)
            {
                state = State.StopRequested;
            }            
        }
    }
}
