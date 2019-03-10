using System.Collections.Generic;
using EnrollmentApi.Logic.Dtos;

namespace EnrollmentLogic.Log
{
    public interface ILogViewRepository
    {
        double? Total();
        int CountOnLevel(string value);  

        LogAggregatedValueDto AggregatedValuesForAll();

        Dictionary<string, int> GetErrorSet();
    }
}