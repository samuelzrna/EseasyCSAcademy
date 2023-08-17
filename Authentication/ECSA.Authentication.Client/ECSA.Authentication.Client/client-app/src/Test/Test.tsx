import React, { useEffect, useState } from 'react';
import './Test.css';

function Test() {
  const [shouldRender, setShouldRender] = useState(false);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setShouldRender(true);
    }, 6000);

    return () => {

      clearTimeout(timeout);
    };
  }, [shouldRender]);

  return (
    <>
      {shouldRender &&
        <div className="container">
          <div className="ring ring1"></div>
          <div className="ring ring2"></div>
          <div className="ring ring3"></div>
          <div className="ring ring4"></div>
          <div className="ring ring5"></div>
          <div className="ring6"></div>
        </div>
      }
    </>
    
  );
}

export default Test;
