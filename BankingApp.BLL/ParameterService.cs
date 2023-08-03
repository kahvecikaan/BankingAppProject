using System.Collections.Generic;
using BankingApp.Domain;
using BankingApp.DAL;

namespace BankingApp.BLL
{
    public class ParameterService
    {
        private readonly ParameterData _parameterData;

        public ParameterService(ParameterData parameterData)
        {
            this._parameterData = parameterData;
        }
        public List<Parameter> FetchParametersByType(string type)
        {
            return this._parameterData.FetchParametersByType(type);
        }

        public List<string> FetchAllAccountTypes()
        {
            return this._parameterData.FetchAllAccountTypes();
        }

        public int GetParameterIdByValue(string type, string description)
        {
            return _parameterData.GetParameterIdByValue(type, description);
        }

        public string GetParameterValueById(string type, int code)
        {
            return _parameterData.GetParameterValueById(type, code);
        }
    }
}
