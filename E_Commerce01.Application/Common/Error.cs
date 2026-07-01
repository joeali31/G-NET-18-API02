namespace E_Commerce01.Application.Common
{
    public record Error(string Code  , string Description , ErrorType ErrorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure", string description = "General failure has occurred") => new(code, description, ErrorType.Failure);
        public static Error Validation(string code = "General.Validation", string description = "General Validation has occurred") => new(code, description, ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string description = "General NotFound has occurred") => new(code, description, ErrorType.NotFound);
        public static Error Conflict(string code = "General.Conflict", string description = "General Conflict has occurred") => new(code, description, ErrorType.Conflict);
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "General Unauthorized has occurred") => new(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "General Forbidden has occurred") => new(code, description, ErrorType.Forbidden);
        public static Error InvalidCredential(string code = "General.InvalidCredential", string description = "General InvalidCredential has occurred") => new(code, description, ErrorType.InvalidCredential);
    }


    public enum ErrorType
    {
        Failure = 0,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        InvalidCredential
    }

}