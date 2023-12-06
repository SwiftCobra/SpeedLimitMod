import React, {useState} from 'react'
import ReactDOM from 'react-dom'

import $Panel from './components/panel'
import $Field from './components/field'
import $Meter from './components/meter'
import $Button from './components/button'

const _components = {
    "panel": $Panel,
    "field": $Field,
    "meter": $Meter,
    "button": $Button,
}

const $InteractiveField = ({react}) => {
    const [checked, setChecked] = react.useState(false)
    return <$Field label="Clickety Rickety" checked={checked} onToggle={setChecked} />
}

const noop = (...args) => {
    console.log('noop', args)
    window.alert("Results: " + JSON.stringify(args, null, 2))
}

const _examples = {
    "panel": [
        <$Panel title="Hello" react={React}>
            <h3>Waddup world</h3>
        </$Panel>
    ],
    "field": [
        <$Field label="Left-hand Traffic" checked={true} onToggle={noop}/>,
        <$Field label="Unlimited Money" checked={false} onToggle={noop}/>,
        <$InteractiveField react={React}/>
    ],
    "meter": [
        <$Meter label="maxGood Test 25" value={25} gradient="maxGood" />,
        <$Meter label="maxGood Test 50" value={50} gradient="maxGood" />,
        <$Meter label="maxGood Test 75" value={75} gradient="maxGood" />,
        <$Meter label="maxGood Test 100" value={100} gradient="maxGood" />,
        <hr/>,
        <$Meter label="minGood Test 25" value={25} gradient="minGood" />,
        <$Meter label="minGood Test 50" value={50} gradient="minGood" />,
        <$Meter label="minGood Test 75" value={75} gradient="minGood" />,
        <$Meter label="minGood Test 100" value={100} gradient="minGood" />,
    ],
    "button": [
        <$Button label="Test Button"/>,
        <$Button label="window.alert" onClick={() => window.alert('hello there')}/>
    ]
}

const $App = () => {
    const [components, setComponents] = useState(_components);
    const [examples, setExamples] = useState(_examples);

    const [selected, setSelected] = useState(null);

    const $components_keys = Object.keys(components).map((k) => {
        const color = selected === k ? "#526DE4" : "#455FCF"
        return <div onClick={() => setSelected(k)}
                    style={{display: "block", color: color}}
                >
            {k} {selected === k ? "<" : ""}
        </div>
    })

    const selected_examples = examples[selected] || []

    const $examples = selected_examples.map((example) => {
        return <div>
            {example}
        </div>
    })

    return <div style={{display: "flex", flexDirection: 'row'}}>
        <div id="sidebar" style={{width: "20%"}}>
            <div><strong>Components</strong></div>
            <div style={{marginTop: 10, marginBottom: 10}}>
                <div>Currently selected:</div>
                <div>{selected || "*None*"}</div>
            </div>
            <div>
                <div>These are the components:</div>
                <div>{$components_keys}</div>
            </div>
        </div>
        <div id="preview" style={{width: "50%"}}>
            <div style={{borderBottom: "2px solid #5d6170"}}><strong>Preview</strong></div>
            <div>{$examples}</div>
        </div>
    </div>
}

ReactDOM.render(<$App/>, document.querySelector('#root'))