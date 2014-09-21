using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public abstract class BaseObject
    {
        [DataMember]
        public Error Error { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Generates a child object from BaseContract with an instance of Error object
        /// </summary>
        /// <typeparam name="T">Child object type</typeparam>
        /// <param name="id">Error Id</param>
        /// <param name="message">Error message</param>
        /// <returns>Instance of T with Error object</returns>
        public virtual T GenerateError<T>(int id, string message) where T : BaseObject, new()
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
        public virtual T GenerateError<T>(int id, string message, Exception ex) where T : BaseObject, new()
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
