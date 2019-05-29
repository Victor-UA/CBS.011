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
            IsCompleted = false;
            Success = false;
        }

        private const string ERROR_MESSAGE_NO_RESULT = "The thread have not received a result yet";

        private readonly Func<T> _threadStart;
        private T _result;

        public bool IsCompleted { get; private set; }
        public bool Success { get; private set; }

        public T Result
        {
            get => Success && IsCompleted ? _result : throw new Exception(ERROR_MESSAGE_NO_RESULT);
            private set => _result = value;
        }

        public void Start()
        {
            try
            {
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

