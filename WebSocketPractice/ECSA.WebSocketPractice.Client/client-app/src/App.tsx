import React, { useEffect, useState } from 'react';
import Chat from './Chat';
import POSImagePlayer from './POSImagePlayer';
import ImagePlayer from './TimeDisplay';

const App: React.FC = () => {

  return (
    <div>
      <div style={{ margin: '0 30%' }}>
        <Chat />
      </div>
    </div>
  );
};

export default App;
