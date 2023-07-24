using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    class ActionService
    {
        private readonly ActionData _actionData;

        public ActionService(ActionData actionData)
        {
            this._actionData = actionData;
        }

        public void InsertAction(Action action)
        {
            this._actionData.InsertAction(action);
        }
        
        public List<Action> FetchActionsByUser(int userId)
        {
            return this._actionData.FetchActionsByUser(userId);
        }

        public List<Action> FetchActionsByCustomer(int customerId)
        {
            return this._actionData.FetchActionsByCustomer(customerId);
        }
    }
}
