import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';

const TimeDisplay: React.FC = () => {
  const [currentTime, setCurrentTime] = useState<string>('');
  const [countdown, setCountdown] = useState<number>(0);
  const [slideIndex, setSlideIndex] = useState<number>(0);

  const [backgroundColor, setBackgroundColor] = useState<string>('');
  const colors: string[] = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#00FFFF', '#FF00FF'];

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7297/clockhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .build();

    connection.start()
      .then(() => {
        console.log('Connected to SignalR hub');
      })
      .catch((error) => {
        console.log('Error connecting to SignalR hub:', error);
      });

    connection.on('ReceiveTime', (time: string) => {
      setCurrentTime(time);

    });

    return () => {
      connection.stop();
    };
  }, []);

  useEffect(() => {
    const timeArray = currentTime.split(':');
    const seconds = parseInt(timeArray[2]);
    if (seconds % 10 === 0) {
      setSlideIndex(slideIndex => (slideIndex + 1) % colors.length);
    }
  }, [currentTime])

  return (
    <div style={{ backgroundColor: `${colors[slideIndex]}` }}>
      <h1>Time Display</h1>
      <p>Current Time: {currentTime}</p>
      <p>Current BGC: {backgroundColor}</p>
      <p>Countdown: {countdown}</p>
    </div>
  );
};

export default TimeDisplay;
