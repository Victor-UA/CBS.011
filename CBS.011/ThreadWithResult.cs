using System;
using System.Threading;

namespace CBS._011
{
    public class ThreadWithResult<T>
    {

        internal ThreadWithResult(Func<T> threadStart)
        {
            _threadStart = threadStart;
            _isCompleted = null;
            Success = false;
            _thread = new Thread(ThreadStart);
        }

        private readonly Thread _thread = null;

        private readonly Func<T> _threadStart;
        private T _result;
        private bool? _isCompleted;

        public bool IsCompleted
        {
            get => _isCompleted ?? false;
            private set => _isCompleted = value;
        }


        public bool Success { get; private set; }

        public T Result
        {
            get => Success && IsCompleted ? _result : throw new Exception("The thread has not received a result yet");
            private set => _result = value;
        }

        public void Start()
        {
            _thread.Start();
        }

        private void ThreadStart()
        {
            IsCompleted = false;
            Success = false;
            try
            {
                Result = _threadStart();
                Success = true;
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}
            finally
            {
                IsCompleted = true;
            }

        }

        public override string ToString()
        {
            var result = "";
            try
            {
                result = Result.ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return $"IsCompleted: {IsCompleted}, Success: {Success}, Result: {result}";
        }        
    }


    public static class ThreadWithResult
    {
        public static ThreadWithResult<T> Create<T>(Func<T> threadStart)
        {
            return new ThreadWithResult<T>(threadStart);
        }
    }

}

