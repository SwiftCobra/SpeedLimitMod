import React from 'react'
import * as styles from '../styles'

const $Button = ({label, onClick}) => {
    return <button className={styles.CLASS_BUTTON} onClick={() => onClick && onClick()}>
        {label}
    </button>
}

export default $Button