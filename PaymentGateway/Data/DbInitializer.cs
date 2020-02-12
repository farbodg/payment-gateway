namespace PaymentGateway.Data
{
    public class DbInitializer
    {
        public static void Initialize(PaymentGatewayContext context)
        {
            context.Database.EnsureCreated();
            return;
        }
    }
}
