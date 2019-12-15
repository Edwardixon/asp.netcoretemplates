import React, { useState, FormEvent } from "react";
import agent from "../api/agent";

interface IProps{
  setToken : (token: string) => void
};

export const Login : React.FC<IProps> = (props) => {
  const [userName, setUsername] = useState("");
  const [password, setPassword] = useState("");


  const handleChange = (e: FormEvent) => {
    const { name, value }: any = e.target;
    if (name === "userName") setUsername(value);
    else setPassword(value);
  };

  const handleSubmit  = async (e: FormEvent) => {
    console.log(userName, password);
    e.preventDefault();
    const body = {
        "userName" : userName,
        "password": password
    }
    try {
        const user = await agent.User.login(body);
        window.localStorage.setItem("jwt", user.token); 
        props.setToken(window.localStorage.getItem("jwt")!);
        // setUsername("");
        // setPassword("");
    }   
    catch (error){
        console.log(error);
    }
  };


  return (
    <form onSubmit={e => handleSubmit(e)}>
      <input
        type="text"
        name="userName"
        value={userName}
        onChange={handleChange}
      />
      <input
        type="password"
        name="password"
        value={password}
        onChange={handleChange}
      />
      <button type="submit">Login</button>
    </form>
  );
};
