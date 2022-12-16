import React, { useState } from "react";
import "./Switch.css";

export default function Switch(props) {
    const[checked, setChecked] = useState(props.isOn || false);
    const handleToggle = (value) => {
        setChecked(value)
        
        if(props.onChange) {
            props.onChange(value)
        }
    };

    return (
        <>
            <input
                checked={checked}
                onChange={(e) => handleToggle(Boolean(e.target.checked ))}
                type="checkbox"
                id={"react-switch-new"}
                className="react-switch-checkbox" />
            <label
                style={{ background: props.isOn && props.onColor }}
                className={(checked ? 'active' : 'inactive') + ' react-switch-label'}
                htmlFor={"react-switch-new"}
            >
                <span className={"react-switch-button"} />
            </label>
        </>);

}