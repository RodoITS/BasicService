using System.ComponentModel;

namespace BasicService.Modules
{

    public enum ErrorCode
    {
            [field: Description("Nessun errore")]
        None = 0,
            [field: Description("Inizializzazione applicazione")]
        Initialization = 1,
            [field: Description("Termine applicazione")]
        Termination = 2,
            [field: Description("Sql")]
        Sql = 3,
            [field: Description("SyncDocuments")]
        SyncDocuments = 4,
            [field: Description("SyncData")]
        SyncData = 5,
            [field: Description("JSONError")]
        JSONError = 6,
            [field: Description("FTP")]
        FTP = 7,
            [field: Description("HTTP")]
        HTTP = 8,
            [field: Description("GenerateDocuments")]
        GenerateDocuments = 9
    }

    public enum ErrorSeverity
    {
            [field: Description("Errore Critico")]
        Critical = 1,
            [field: Description("Warning")]
        Warn = 2,
            [field: Description("Informazione")]
        Info = 3
    }

    public enum ErrorSource
    {
            [field: Description("Server")]
        Server,
            [field: Description("Client")]
        Client
    }


}