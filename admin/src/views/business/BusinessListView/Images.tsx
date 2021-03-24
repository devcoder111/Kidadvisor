import React, {
  useState,
  useEffect,
  useCallback
} from 'react';
import type { FC } from 'react';
import {
  Card,
  CardContent,
  CardHeader,
  Divider,
  Box,
  Container,
  makeStyles
} from '@material-ui/core';
import ImagesDropzone from 'src/components/ImagesDropzone';
import axios from 'axios';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import type { Business } from 'src/types/business';
import Header from './Header';
import Results from './Results';
import { useDispatch, useSelector } from 'src/store';
import { getBusinesses } from 'src/slices/business';
import ImagesList from 'src/components/ImagesList';
import imagesFetch from 'src/apis/FetchImagesApi';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3)
  }
}));

const Images: FC = () => {
  const classes = useStyles();
  const { businesses } = useSelector((state) => state.businesses);
  const dispatch = useDispatch();

  const refreshBusinessList = () => {
    dispatch(getBusinesses());
  }
  useEffect(() => {
    dispatch(getBusinesses());
  }, [dispatch]);

  const isMountedRef = useIsMountedRef();
  const [images, setImages] = useState<any[]>([]);

  const getImages = useCallback(async () => {
    try {
      const response = await imagesFetch();
      if (isMountedRef.current) {
        setImages(response.list);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getImages();
  }, [getImages]);


  return (
    <Page
      className={classes.root}
      title="Customer List"
    >
      <Container maxWidth={false}>
        <Box mt={3}>
          <Card>
            <CardHeader title="Upload Images" />
            <Divider />
            <CardContent>
              <ImagesDropzone updateImages={getImages} />
            </CardContent>
          </Card>
        </Box>
        <Box mt={3}>
          <Card>
            <CardHeader title="Uploaded Images" />
            <Divider />
            <CardContent>
              <ImagesList images={images} />
            </CardContent>
          </Card>
        </Box>
      </Container>
    </Page>
  );
};

export default Images;
