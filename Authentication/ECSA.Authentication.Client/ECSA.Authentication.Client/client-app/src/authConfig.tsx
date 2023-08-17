import { Configuration } from "@azure/msal-browser";
var { PublicClientApplication } = require("@azure/msal-browser");

const msalConfig: Configuration = {
  auth: {
    clientId: "3440cf35-a64c-4d25-a637-1801b6eb0623",
    redirectUri: "https://localhost:7275", // Replace with your redirect URI
    authority: 'https://login.microsoftonline.com/a063685b-e4e2-4f6c-81f4-b6755151a472', // Replace with your authority URL
  },
  cache: {
    cacheLocation: "localStorage", // You can choose "localStorage" or "sessionStorage" based on your preference
  },
  /*system: {
    loggerOptions: {
      loggerCallback: function (level, message, containsPii) {
        if (containsPii) {
          return;
        }
        // console.log(level + " - " + message);
      },
      logLevel: LogLevel.Verbose,
    },
  },*/
};

export const msalAuthProvider = {
  instance: new PublicClientApplication(msalConfig),
  scopes: ["openid", "profile", "User.Read"], // Add any additional scopes your app requires
};
