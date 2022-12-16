using ResourceManagement_Fe_App.Data;

namespace ResourceManagement_Fe_App.Helpers
{
    public static class UIHelper
    {
        public static string GetColor(TransactionType type)
        {
            switch (type)
            {
                case TransactionType.Outgoing: return "volcano";
                case TransactionType.Incoming: return "green";
                default: return "geekblue";
            }
        }

        public static string GetName(TransactionType type)
        {
            switch (type)
            {
                case TransactionType.Outgoing: return "Uscita";
                case TransactionType.Incoming: return "Entrata";
                default: return "Non Specificata";
            }
        }
    }
}
