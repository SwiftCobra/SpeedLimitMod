import React from 'react'
import { $Panel, $Field} from 'hookui-framework'

const $CityMonitor = ({ react }) => {
    const [speedLimitEditor, setSpeedLimitEditor] = react.useState(false)

    const handleToggle = (k, newValue) => {
        engine.trigger('speed_limit_editor.toggle_speed_limit_editor', k, newValue);
        setSpeedLimitEditor(newValue);
    }

    const style = {
        height: "auto"
    }

    return <div>
        <$Panel title="Speed Limit Editor" react={react} style={style}>
            <$Field label="Speed Limit Editor" checked={fastBuild} onToggle={(newCheckedValue) => {
                handleToggle("debugFastSpawn", newCheckedValue);
            } } />
        </$Panel>
    </div>
}

window._$hookui.registerPanel({
    id: "example.speed-limit-editor",
    name: "Fast Build",
    icon: "Media/Game/Icons/Zones.svg",
    component: $CityMonitor
})