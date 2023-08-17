import React, { useEffect, useState } from "react";
import './CircleExpand.css';

const CircleExpand = () => {
  return (
    <div className="fullscreen-container">
      <div className="center-line horizontal"></div>
      <div className="center-line vertical"></div>

      <div className="circle-container">
        <div className="circle">
          <div className="center-line-green horizontal"></div>
          <div className="center-line-green vertical"></div>
        </div>
      </div>

      
    </div>
  );
};

export default CircleExpand;
