import React, {
  useState,
  useEffect,
  useCallback
} from 'react';
import type { FC } from 'react';
import {
  Box,
  Container,
  makeStyles
} from '@material-ui/core';
import axios from 'axios';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import type { Business } from 'src/types/business';
import Header from './Header';
import Results from './Results';
import { useDispatch, useSelector } from 'src/store';
import { getBusinesses } from 'src/slices/business';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3)
  }
}));

const BusinessListView: FC = () => {
  const classes = useStyles();
  const { businesses } = useSelector((state) => state.businesses);
  const dispatch = useDispatch();

  const refreshBusinessList = () => {
    dispatch(getBusinesses());
  }
  useEffect(() => {
    dispatch(getBusinesses());
  }, [dispatch]);

  return (
    <Page
      className={classes.root}
      title="Customer List"
    >
      <Container maxWidth={false}>
        <Header />
        <Box mt={3}>
          <Results businesses={businesses} deleted={refreshBusinessList}/>
        </Box>
      </Container>
    </Page>
  );
};

export default BusinessListView;
