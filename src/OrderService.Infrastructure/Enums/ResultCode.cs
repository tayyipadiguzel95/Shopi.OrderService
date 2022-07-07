namespace OrderService.Infrastructure.Enums
{
    /// <summary>
    /// ResultCode
    /// </summary>
    public enum ResultCode
    {
        Exception = 0,
        Success = 1,
        NoContent = 2,
        AuthorizationError = 3,
        Warning = 5,
        ForbiddenError = 6,
        OtpVerification = 7,
        PaymentError = 8,
        ValidationError = 9
    }
}