import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

const DisplaySync: React.FC = () => {
  const [counter, setCounter] = useState(0);
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7297/displaySync')
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start().then(() => {
        console.log("connected")
      }).catch((error: Error) => console.error(error));

      connection.on('UpdateCounter', (value: number) => {
        console.log("update counter called")
        setCounter(value);
      });

      return () => {
        connection.stop();
      };
    }
  }, [connection]);

  return (
    <div>
      <h1>Counter: {counter}</h1>
    </div>
  );
};

export default DisplaySync;