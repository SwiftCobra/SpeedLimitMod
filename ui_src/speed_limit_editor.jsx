import React from 'react'
import { useDataUpdate } from 'hookui-framework'
import $Panel from './panel'


//Taken from Cities2Modding and modified to allow units and min/max ranges
const $Slider = ({ react, value, units, onValueChanged, style, min = 0, max = 100 }) => {
    const sliderRef = react.useRef(null); // Reference to the slider element

    const handleSliderClick = (e) => {
        const slider = sliderRef.current;

        if (!slider) return;

        // Get the click position relative to the slider
        const rect = slider.getBoundingClientRect();
        const clickedPosition = e.clientX - rect.left;

        // Calculate the new value based on click position
        let newValue = (clickedPosition / rect.width) * (max - min) + min;

        // Round to the nearest multiple of 5
        newValue = Math.round(newValue / 2) * 2;
        newValue = parseInt(newValue, 10);

        // Clamp the new value between min and max
        newValue = Math.min(max, Math.max(min, newValue));
        if (onValueChanged) {
            onValueChanged(parseInt(newValue, 10));
        }
    };

    const valuePercent = ((value - min) / (max - min)) * 100 + "%";

    return (
        <div style={{ width: '100%', ...style }}>
            <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', justifyContent: 'center', margin: '10rem', marginTop: '0' }}>
                <div className="value_jjh" style={{ display: 'flex', width: '45rem', alignItems: 'center', justifyContent: 'center' }}>{units === '' ? `${value}` : `${value} ${units}`}</div>
                <div
                    className="slider_fKm slider_pUS horizontal slider_KjX"
                    style={{
                        flex: 1,
                        margin: '10rem',
                    }}
                    ref={sliderRef}
                    onClick={handleSliderClick}
                >
                    {/* Progressbar fill */}
                    <div className="track-bounds_H8_">
                        <div className="range-bounds_lNt" style={{ width: valuePercent }}>
                            <div className="range_KXa range_iUN"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};


const $SpeedLimitEditor = ({ react }) => {
    const [speed, setSpeed] = react.useState(1);
    const [name, setName] = react.useState('');
    const [units, setUnits] = react.useState('');

    useDataUpdate(react, "speed_limit_editor.speed", setSpeed);
    useDataUpdate(react, "speed_limit_editor.name", setName);
    useDataUpdate(react, "speed_limit_editor.unitSystem", setUnits);

    const handleSpeedChange = (s) => {
        engine.trigger('speed_limit_editor.set_speed_limit', s);
        setSpeed(s);
    }

    const onClose = (e) => {
        const data = { type: "toggle_visibility", id: "scobra.speed-limit-editor" };
        const event = new CustomEvent('hookui', { detail: data });
        window.dispatchEvent(event);
    }

    const style = {
        height: "300"
    }

    let minMax = { min: 0, max: 200 };
    if (units === 'mph')
        minMax = { min: 0, max: 124}

    return <div>
        <$Panel title="Speed Limit Editor" react={react} style={style} onClose={onClose}>
            {name === '' && speed === 0 ? <h2 style={{ marginLeft: '1em' }}>No Road Selected</h2> : null }
            {name === '' ? null : <h2 style={{marginLeft: '1em'} }>{name}</h2> }
            {speed == 0 ? null :
                <$Slider react={react} value={speed} units={units} min={minMax.min} max={minMax.max} onValueChanged={(val) => handleSpeedChange(val)} />
            }
        </$Panel>
    </div>
}

window._$hookui.registerPanel({
    id: "scobra.speed-limit-editor",
    name: "Speed Limit",
    icon: "Media/Game/Policies/HighSpeedHighways.svg",
    component: $SpeedLimitEditor
})