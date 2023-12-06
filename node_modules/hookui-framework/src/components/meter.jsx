import React from 'react'
import * as styles from '../styles'

// Used when max value (right) indicates that everything is OK ("Electricity Availability" for example)
const maxGood = 'linear-gradient(to right,rgba(255, 78, 24, 1.000000) 0.000000%, rgba(255, 78, 24, 1.000000) 40.000000%, rgba(255, 131, 27, 1.000000) 40.000000%, rgba(255, 131, 27, 1.000000) 50.000000%, rgba(99, 181, 6, 1.000000) 50.000000%, rgba(99, 181, 6, 1.000000) 60.000000%, rgba(71, 148, 54, 1.000000) 60.000000%, rgba(71, 148, 54, 1.000000) 100.000000%)'
// Used when min value (left) indicates everything is fine ("Fire Hazard" for example)
const minGood = 'linear-gradient(to right,rgba(71, 148, 54, 1.000000) 0.000000%, rgba(99, 181, 6, 1.000000) 66.000000%, rgba(255, 131, 27, 1.000000) 33.000000%, rgba(255, 78, 24, 1.000000) 100.000000%)'

const gradients = {
    maxGood, minGood
}

const $Meter = ({label, value, gradient}) => {
    if (gradients[gradient] === undefined) {
        throw new Error(`Couldn't find gradient ${gradient}, check list of gradients for possible values`)
    }
    const gradientStyle = {
        "backgroundImage": gradients[gradient]
    }
    const pointerStyle = {
        left: value + "%"
    }
    return <div className={styles.CLASS_INFOVIEWPANELSECTION}>
        <div className={styles.many(styles.CLASS_METER_CONTENT, styles.CLASS_FOCUSABLE, styles.CLASS_ITEMFOCUSED)}>
            <div className={styles.many(styles.CLASS_LABELS, styles.CLASS_ROW)}>
                <div className={styles.many(styles.CLASS_UPPERCASE, styles.CLASS_LEFT, styles.CLASS_ROW)}>
                    {label}
                </div>
            </div>
            <div className={styles.CLASS_BAR}>
                <div className={styles.CLASS_GRADIENT} style={gradientStyle}></div>
                <div className={styles.CLASS_POINTER} style={pointerStyle}>
                    <img className={styles.CLASS_POINTERICON} src="Media/Misc/IndicatorBarPointer.svg"></img>
                </div>
            </div>
            {/* <div className="labels_L7Q row_S2v tiny_m9B">
                <div className="left_Lgw row_S2v">Consumption: 50.8 kW</div>
                <div className="right_k3O row_S2v">Production: 0 kW</div>
            </div> */}
            <div className={styles.CLASS_SMALLSPACE}></div>
        </div>
    </div>
}


export default $Meter