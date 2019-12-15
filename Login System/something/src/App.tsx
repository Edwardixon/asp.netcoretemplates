import React, { Fragment, useState } from "react";
import { Login } from "./form/Login";
import agent from "./api/agent";

const App: React.FC = () => {

  const [secret, setSecret] = useState("");
  const handleClick = async () => {
    try {
      const yes = await agent.User.getSecret();
      setSecret(yes);
    }catch(error){
      console.log("You are not loged in");
    }
  }

  const handleLogOut = () => {
    window.localStorage.clear();
    setToken("");
  }

  const [token, setToken] = useState(window.localStorage.getItem("jwt"));

  return (
    <Fragment>
      {token ? (
        <Fragment>
          <h1>You are now loged in!</h1>
          <h2>{secret}</h2>
          <button onClick={handleClick}>Get secret</button>
          <button onClick={handleLogOut}>Log out</button>
        </Fragment>
      ) : (
        <Fragment>
          <Login setToken={setToken} />
          <button onClick={handleClick}>Get secret</button>
        </Fragment>
      )}
    </Fragment>
  );
};

export default App;
