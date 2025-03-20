using Grpc.Core;

namespace GrpcEmailSender.API.Services
{
    public class EmailSenderService : EmailSender.EmailSenderBase
    {
        public override Task<EmailResponse> SendEmail(EmailRequest request, ServerCallContext context)
        {
            var response = new EmailResponse()
            {
                Status = "00",
                Message = "Email sent successfully"
            };

            return Task.FromResult(response);
        }
    }
}
