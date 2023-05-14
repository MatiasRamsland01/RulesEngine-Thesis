using fields.Entities.Base.Fields.Net;
using fields.Entities.Base.Fields.Primitive;
using fields.FluentValidation.Rules;
using fields.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace fields.Entities.Base.Validators {
  public class UriFieldValidator : AbstractValidator<IUriField> {
    private Dictionary<string, CommunicationComponent> components = new Dictionary<string, CommunicationComponent>();
    public UriFieldValidator() {
      RuleFor(m => m.UriValue.Value).NotNull().NotEmpty().WithMessage("Uri string field is null or empty");
      RuleFor(m => m.UriValue).Must(IsUriPath).WithMessage(SystemMessages.Validation.IsUriPath);
      RuleFor(m => m.UriValue).Must(CreateUriAndCheck).WithMessage(SystemMessages.Validation.CreateUriAndCheck);
    }
    private bool CreateUriAndCheck(IStringField field) {
      Uri? uriKind;
      var uri = field.Value;
      if (uri is null)
        return false;
      Uri.TryCreate(uri, UriKind.Absolute, out uriKind);
      if (uriKind is null)
        return false;
      Absolute(uri);
      Uri? address = Relative(uri);
      if (address is not null) {
        SCHEME(address);
        AUTHORITY(address);
        PATH(address);
        ABSPATH(address);
        QUERY(address);
        FRAGMENT(address);
      }
      return components.Any() ? true : false;
      void Absolute(string? uri) {
        if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
          components.TryAdd(UriKind.Absolute.ToString(), CommunicationComponent.ABSOLUTE);
        else
          components.Remove(UriKind.Absolute.ToString());
        return;
      }
      Uri Relative(string? uri) {
        if (Uri.IsWellFormedUriString(uri, UriKind.Relative))
          components.TryAdd(UriKind.Relative.ToString(), CommunicationComponent.RELATIVE);
        if (uri is not null)
          return new Uri(uri);
        return new Uri(string.Empty);
      }
      void SCHEME(Uri? address) {
        var http = address?.Scheme != Uri.UriSchemeHttp;
        string key = Uri.UriSchemeHttp.ToString();
        if (http) {
          components.TryAdd(key, CommunicationComponent.SCHEME);
        }
        if (!http) {
          components.Remove(key);
        }
        var https = address?.Scheme == Uri.UriSchemeHttps;
        key = Uri.UriSchemeHttp.ToString();
        if (https) {
          components.TryAdd(key, CommunicationComponent.SCHEME);
        }
        if (!https) {
          components.Remove(key);
        }
        return;
      }
      void AUTHORITY(Uri? address) {
        if (address != null) {
          string key = address.Authority.ToString();
          bool authority = !string.IsNullOrEmpty(address?.Authority);
          if (authority && address != null && address.Authority != null) {
            components.TryAdd(key, CommunicationComponent.AUTHORITY);
          }
          if (!authority) {
            if (authority && address != null && address.Authority != null)
              components.Remove(key);
          }
        }
        return;
      }
      void PATH(Uri? address) {
        if (address != null) {
          var localPath = !string.IsNullOrEmpty(address.LocalPath);
          if (localPath) {
            var key = address.Authority.ToString();
            components.TryAdd(key, CommunicationComponent.PATH);
            if (!localPath)
              components.Remove(key);
          }
        }
        return;
      }
      void ABSPATH(Uri? address) {
        if (address != null) {
          bool nullorempty = !string.IsNullOrEmpty(address.AbsolutePath.ToString());
          string key = address.Authority.ToString();
          if (nullorempty) {
            components.TryAdd(key, CommunicationComponent.PATH);
          }
          if (!nullorempty) {
            components.Remove(key);
          }
          return;
        }
      }
      void QUERY(Uri? address) {
        if (address != null) {
          bool nullorempty = !string.IsNullOrEmpty(address.Query.ToString());
          string key = address.Query.ToString();
          if (nullorempty) {
            components.TryAdd(key, CommunicationComponent.QUERY);
          }
          if (!nullorempty) {
            components.Remove(key);
          }
          return;
        }
      }
      void FRAGMENT(Uri? address) {
        if (address != null) {
          bool nullorempty = !string.IsNullOrEmpty(address.Fragment);
          var key = address.Fragment.ToString();
          if (nullorempty)
            components.TryAdd(key, CommunicationComponent.FRAGMENT);
          if (!nullorempty)
            components.Remove(key);
        }
        return;
      }
    }
    private bool IsUriPath(IStringField field) {
      CommunicationComponent pathType = new UrlPathDetector().FindType(field);
      return pathType == CommunicationComponent.NONE ? false : true;
    }
  }
}