import React from 'react';

interface IProps {
  user: string;
  message: string;
}


const Message = (props: IProps) => (
  <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
    <p><strong>{props.user}</strong> says:</p>
    <p>{props.message}</p>
  </div>
);

export default Message;