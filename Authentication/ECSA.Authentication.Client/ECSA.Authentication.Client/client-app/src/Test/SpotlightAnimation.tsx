import React from 'react';

const centeredVideoStyle: React.CSSProperties = {
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  minHeight: '100vh',
  margin: 0,
};

const videoStyle: React.CSSProperties = {
  maxWidth: '100%',
};

const SpotlightAnimation = () => {
  return (
    <div style={centeredVideoStyle}>
      <video autoPlay muted style={videoStyle}>
        <source src="https://ibcdigitalsignageblob.blob.core.windows.net/fe3c4826-4e72-4cd4-a938-382d67a8badf/assets/Untitled_video_-_Made_with_Clipchamp_%286%29.mp4" type="video/mp4" />
        Your browser does not support the video tag.
      </video>
    </div>
  );
}

export default SpotlightAnimation;
