import React, { useEffect, useRef, useState } from 'react';
import * as signalR from '@microsoft/signalr';

interface IProps {
  posImages: string[];
}

const POSImagePlayer = (props: IProps) => {

  const { posImages } = props;
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [connectionId, setConnectionId] = useState<string | null>("");
  const [clientPlaylists, setClientPlaylists] = useState<Record<string, string[]>>({});

  useEffect(() => {
    const hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7297/posImageHub') 
      .build();

    const getConnectionId = async () => {
      try {
        const connectionId = await hubConnection.invoke('ReceiveConnectionId');
        setConnectionId(connectionId);
      } catch (error) {
        console.error('Error calling ReceiveConnectionId:', error);
      }
    };

    hubConnection.start().then(() => {
      getConnectionId();
    }).catch((error) => console.error('SignalR Connection Error: ', error));

    hubConnection.onclose(async () => { 
      await hubConnection.start();
      getConnectionId();
    })
    

    /*hubConnection.on('ReceivePlaylist', (playlist: string[]) => {
      setClientPlaylists((prevPlaylists) => ({
        ...prevPlaylists,
        [hubConnection.connectionId || '']: playlist
      }));
    });*/
   
    return () => {
      if (hubConnection.state === signalR.HubConnectionState.Connected) {
        hubConnection.stop();
      }
    };
  }, []);

  /*useEffect(() => {
    if (connection) connection.send('SetPlaylist', posImages);
  }, [connection]);*/

  return (
    <div>
      <div>{connectionId}</div>
      <ul>
        {posImages.map(img => {
          return <li key={img}>{img}</li>
        })}
      </ul>
    </div>
  );
};

export default POSImagePlayer;
