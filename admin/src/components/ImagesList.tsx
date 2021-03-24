import React from 'react';
import type { FC } from 'react';
import { makeStyles, Theme } from '@material-ui/core';

const useStyles = makeStyles((theme: Theme) => ({
    root: {},
    uploadedImage: {
        width: 300,
        marginLeft: 10
    }
}));

interface LogoProps {
    [key: string]: any;
}

const ImagesList: FC<LogoProps> = (props) => {
    const classes = useStyles();
    return (
        <div className="wrap-images">
            {props.images.map((x) => {
                return <img
                className={classes.uploadedImage}
                alt="Logo"
                src={x}
            />
            })}
        </div>
    );
}

export default ImagesList;