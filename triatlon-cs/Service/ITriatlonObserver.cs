using System.Collections.Generic;
using Triatlon.Proto;

namespace Service
{
    public interface ITriatlonObserver
    {
        void ResultAdded(List<Result> results);
    }
}