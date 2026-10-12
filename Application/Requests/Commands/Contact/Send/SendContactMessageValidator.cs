using FluentValidation;

namespace Application.Requests.Commands.Contact.Send;

public class SendContactMessageValidator : AbstractValidator<SendContactMessageCommand>
{
    public SendContactMessageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Imię jest wymagane.")
            .MaximumLength(100).WithMessage("Imię nie może przekraczać 100 znaków.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Podaj poprawny adres email.")
            .MaximumLength(200).WithMessage("Email nie może przekraczać 200 znaków.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Temat jest wymagany.")
            .MaximumLength(200).WithMessage("Temat nie może przekraczać 200 znaków.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Wiadomość jest wymagana.")
            .MaximumLength(2000).WithMessage("Wiadomość nie może przekraczać 2000 znaków.");
    }
}
