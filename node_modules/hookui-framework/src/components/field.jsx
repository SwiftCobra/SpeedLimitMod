import React from 'react'
import * as styles from '../styles'

const $Field = ({label, react, checked, onToggle}) => {
    const checked_class = checked ? styles.CLASS_CHECKED : styles.CLASS_UNCHECKED
    
    const handleClick = () => {
        onToggle(!checked)
    }

    return <div className={styles.many(styles.CLASS_FIELD, styles.CLASS_TOGGLEITEM)} onClick={handleClick}>
        <div className={styles.CLASS_LABEL}>
            {label}
        </div>
        <div className={styles.many(styles.CLASS_TOGGLE, styles.CLASS_ITEMMOUSESTATES, checked_class)}>
            <div className={styles.many(styles.CLASS_CHECKMARK, checked_class)}></div>
        </div>
    </div>
}

export default $Field