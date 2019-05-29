using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CBS._011
{
    public class ThreadWithResult<T>
    {

        internal ThreadWithResult(Func<T> threadStart)
        {
            _threadStart = threadStart;
            _isCompleted = null;
            Success = false;
        }


        private readonly Func<T> _threadStart;
        private T _result;
        private bool? _isCompleted;

        /// <summary>
        /// false: is running,
        /// true: is completed,
        /// null: has not started yet;
        /// </summary>
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
            IsCompleted = false;
            try
            {
                Success = false;
                Result = _threadStart();
                Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");         
            }
            IsCompleted = true;
        }

        public override string ToString()
        {
            string result = "";
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

