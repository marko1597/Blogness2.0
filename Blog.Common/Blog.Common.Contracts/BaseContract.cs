using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public abstract class BaseContract
    {
        [DataMember]
        public Error Error { get; set; }

        /// <summary>
        /// Generates a child object from BaseContract with an instance of Error object
        /// </summary>
        /// <typeparam name="T">Child object type</typeparam>
        /// <param name="id">Error Id</param>
        /// <param name="message">Error message</param>
        /// <param name="ex">Exception object</param>
        /// <returns>Instance of T with Error object</returns>
        public virtual T GenerateError<T>(int id, string message) where T : BaseContract, new()
        {
            return new T
            {
                Error = new Error
                {
                    Id = id,
                    Message = message
                }
            };
        }

        /// <summary>
        /// Generates a child object from BaseContract with an instance of Error object
        /// </summary>
        /// <typeparam name="T">Child object type</typeparam>
        /// <param name="id">Error Id</param>
        /// <param name="message">Error message</param>
        /// <param name="ex">Exception object</param>
        /// <returns>Instance of T with Error object</returns>
        public virtual T GenerateError<T>(int id, string message, Exception ex) where T : BaseContract, new()
        {
            return new T
            {
                Error = new Error
                {
                    Id = id,
                    Message = message,
                    Exception = ex
                }
            };
        }
    }
}
