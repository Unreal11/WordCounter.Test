import { useState } from 'react'
import './TextField.css'

export default function TextField(props) {
    const [state, setState] = useState({
        timeout: undefined
    });

    const invokeTimer = (text) => {
        if(state.timeout) {
            clearTimeout(state.timeout);
        }

        state.timeout = setTimeout(() => { 
            if(props.onChange) {
                props.onChange(text);
            }
        }, props.interval || 1000);
        
    };

    return (
        <div className='text-field'>
            <input onChange={(e) => { invokeTimer(e.target.value) }}>
            </input>
        </div>
    )
}