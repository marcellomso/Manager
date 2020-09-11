using Manager.Domain.Contracts.Repositories;
using System;

namespace Manager.Service
{
    public class ServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Commit()
        {
            _unitOfWork.Commit();
            return true;
        }

        protected void ValidateObject(object obj, string msg)
        {
            if (obj == null)
                throw new Exception(msg);
        }
    }
}
