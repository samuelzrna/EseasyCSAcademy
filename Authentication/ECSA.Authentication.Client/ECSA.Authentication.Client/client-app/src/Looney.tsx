import React from "react";
import './Looney.css'
import { MsalProvider, AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";

const Looney = () => {
  const { instance, accounts } = useMsal();

  const handleSignOut = () => {
    if (accounts.length > 0) {
      instance.logoutRedirect({ postLogoutRedirectUri: "/" });
    }
  };

  return (
    <body>
      <div className="container">
        <div className="ring ring1"></div>
        <div className="ring ring2"></div>
        <div className="ring ring3"></div>
        <div className="ring ring4"></div>
        <div className="ring ring5"></div>
        <div className="ring6"></div>
      </div>
    </body>
  );
};

export default Looney;
