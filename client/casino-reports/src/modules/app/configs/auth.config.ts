import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  // Url of the Identity Provider
  issuer: 'https://localhost:44300',

  // Defines whether to use OpenId Connect during implicit flow.
  oidc: true,

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'AngularSPA',

  // set the scope for the permissions the client should request
  // The first five are defined by OIDC. The rest are usecase-specific
  scope: 'openid profile email address phone roles CasinoReportsAPI',

  /**
   * Defines whether to request an access token during
   * implicit flow.
   */
  requestAccessToken: true,

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin,

  // URL of the SPA to redirect the user after silent refresh
  silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',

  // URL of the SPA to redirect the user to after logout
  postLogoutRedirectUri: window.location.origin + '/logout',

  /**
   * Set this to true to display the iframe used for
   * silent refresh for debugging.
   */
  // silentRefreshShowIFrame: true,

  /**
   * Defines whether additional debug information should
   * be shown at the console. Note that in certain browsers
   * the verbosity of the console needs to be explicitly set
   * to include Debug level messages.
   */
  // showDebugInformation: true,

  /**
   * If true, the lib will try to check whether the user
   * is still logged in on a regular basis as described
   * in http://openid.net/specs/openid-connect-session-1_0.html#ChangeNotification
   */
  // sessionChecksEnabled: true,
};
