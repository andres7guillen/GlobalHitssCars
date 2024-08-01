using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceDomain.Exceptions
{
    public static class ClientContextExceptions
    {
        public static readonly Dictionary<ClientContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { ClientContextExceptionEnum.ClientNotFound, new Tuple<int, string>((int)ClientContextExceptionEnum.ClientNotFound, "Client not found")},
        { ClientContextExceptionEnum.ClientNotFoundByFilter, new Tuple<int, string>((int)ClientContextExceptionEnum.ClientNotFoundByFilter, "Client not found by filter") },
        { ClientContextExceptionEnum.ErrorUpdatingClient, new Tuple<int, string> ((int)ClientContextExceptionEnum.ErrorUpdatingClient, "Error updating Client") },
        { ClientContextExceptionEnum.ErrorCreatingClient, new Tuple<int, string>((int)ClientContextExceptionEnum.ErrorCreatingClient, "Error creating Client") },
        { ClientContextExceptionEnum.ErrorDeleteingClient, new Tuple<int, string>((int)ClientContextExceptionEnum.ErrorDeleteingClient, "Error deleteing Client") },
        { ClientContextExceptionEnum.NoClientsFound, new Tuple<int, string>((int)ClientContextExceptionEnum.NoClientsFound, "No Clients found") },
        { ClientContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)ClientContextExceptionEnum.UndefinedError, "Undefined error") }

    };
    }

    public enum ClientContextExceptionEnum
    {
        //4000
        ClientNotFound = 4000,
        ClientNotFoundByFilter = 4001,
        ErrorUpdatingClient = 4002,
        ErrorCreatingClient = 4003,
        ErrorDeleteingClient = 4004,
        NoClientsFound = 4005,
        UndefinedError = 4006
    }
    public static class ClientContextExceptionEnumExtensions
    {
        public static string GetErrorMessage(this ClientContextExceptionEnum error)
        {
            if (ClientContextExceptions.ErrorMessages.TryGetValue(error, out var message))
            {
                return $"{message.Item1}: {message.Item2}";
            }
            return $"{(int)error}: Unknown error";
        }
    }
}
