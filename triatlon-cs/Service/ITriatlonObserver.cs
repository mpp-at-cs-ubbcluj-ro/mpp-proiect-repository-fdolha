using System.Collections.Generic;
using Model;

namespace Service
{
    public interface ITriatlonObserver
    {
        void ResultAdded(List<Result> results);
    }
}