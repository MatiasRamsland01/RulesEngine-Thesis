using fields.FluentValidation.Rules;

public static class UrlPathDetectorHelpers {

  public static CommunicationComponent MapNumberGroupsToEnum(int matches) {
    return matches switch {
      0 => CommunicationComponent.NONE,
      1 => CommunicationComponent.SCHEME,
      2 => CommunicationComponent.AUTHORITY,
      3 => CommunicationComponent.PATH,
      4 => CommunicationComponent.QUERY,
      5 => CommunicationComponent.FRAGMENT,
      _ => CommunicationComponent.NONE,
    };
  }
}