import React, {
  useState,
  useCallback,
  useEffect
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
import BusinessEditForm from './BusinessEditForm';
import Header from './Header';
import {RouteComponentProps} from "react-router";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3)
  }
}));

const BusinessEditView: FC<RouteComponentProps> = (props) => {
  const classes = useStyles();
  const isMountedRef = useIsMountedRef();
  const [business, setBusiness] = useState<Business | null>(null);
  const [businessId, setBusinessId] = useState<number | null>(null);

  const getBusiness = useCallback(async (id) => {
    try {
      const response = await axios.get('https://localhost:5001/api/v1/businesses/' + id);
    
      if (isMountedRef.current) {
        setBusiness(response.data);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    const id = props.match.params['businessId'];
    setBusinessId(id);
    if(id) {
      getBusiness(id);
    } else {
      const data: Business = {
        businessId : '',
        name : '',
        description : '',
        streetAddress : '',
        appartement : '',
        city : '',
        province: '',
        country : '',
        postalCode : '',
      };
      setBusiness(data);
    }
  }, [getBusiness]);

  if (!business) {
    return null;
  }

  return (
    <Page
      className={classes.root}
      title={businessId ? 'Business Edit' : 'Business Create'}
    >
      <Container maxWidth={false}>
        <Header businessId={businessId} />
      </Container>
      <Box mt={3}>
        <Container maxWidth="lg">
          <BusinessEditForm business={business} />
        </Container>
      </Box>
    </Page>
  );
};

export default BusinessEditView;
