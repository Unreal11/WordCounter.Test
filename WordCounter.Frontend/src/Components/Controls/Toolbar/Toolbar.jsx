import './Toolbar.css'
import { SimpleDropdown } from 'react-js-dropdavn'
import 'react-js-dropdavn/dist/index.css'
import { useState } from 'react'
import TextField from '../TextField/TextField'
import Switch from '../Switch/Switch'

const options = Array(25).fill().map((_, i) => { return { 
    label: i * 5 + 5, 
    value: i * 5 + 5 
}})

export default function Toolbar(props) {
    const [state, setState] = useState({
        removeGrammar: false
    });

    const invokeChange = (data) => {
        let newState = { ...state }

        Object.keys(data).forEach(key => {
            newState[key] = data[key];
        });

        setState(newState);

        if(props.onChange) {
            props.onChange(newState);
        }
    }

    return (
        <div className='toolbar'>
            <div className='column medium'>
                <p>Remove grammars</p>
                <Switch onChange={(value) =>  invokeChange({ removeGrammar: value})}/>
            </div>
            <div className='column full'>
                <TextField onChange={ (value) => invokeChange({ url: value })}/>
            </div>
            <div className='column'>
                <SimpleDropdown
                    options={options}
                    defaultValue={ state.groupsCount }
                    configs={
                        { position: { y: 'bottom', x: 'center' } }
                    }
                    onChange = { (value) => invokeChange({ groupsCount:value.value }) }
                    />
            </div>
        </div>);
}