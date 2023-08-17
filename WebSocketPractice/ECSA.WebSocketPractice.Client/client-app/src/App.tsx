import React, { useEffect, useState } from 'react';
import Chat from './Chat';
import DisplaySync from './DisplaySync';
import POSImagePlayer from './POSImagePlayer';
import ImagePlayer from './TimeDisplay';

const App: React.FC = () => {

  return (
    <div>
      <DisplaySync />
    </div>
  );
};

export default App;
