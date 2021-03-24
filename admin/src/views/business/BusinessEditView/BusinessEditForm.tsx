import React from 'react';
import type { FC } from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import * as Yup from 'yup';
import { Formik } from 'formik';
import axios from 'axios';
import { useSnackbar } from 'notistack';
import {
  Box,
  Button,
  Card,
  CardContent,
  Grid,
  Switch,
  TextField,
  Typography,
  makeStyles
} from '@material-ui/core';
import type { Business } from 'src/types/business';

import { useDispatch } from 'src/store';
import { addBusiness } from 'src/slices/business';
import { updateBusiness } from 'src/slices/business';

interface CustomerEditFormProps {
  className?: string;
  business: Business;
}

const useStyles = makeStyles(() => ({
  root: {}
}));

const BusinessEditForm: FC<CustomerEditFormProps> = ({
  className,
  business,
  ...rest
}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const dispatch = useDispatch();

  return (
    <Formik
      initialValues={{
        id: business.id || 0,
        name : business.name || '',
        description : business.description || '',
        streetAddress : business.streetAddress || '',
        appartement : business.appartement || '',
        city : business.city || '',
        province: business.province || '',
        country : business.country || '',
        postalCode : business.postalCode || '',

        submit: null
      }}
      validationSchema={Yup.object().shape({
        name: Yup.string().max(255).required('Business Name is required'),
        description: Yup.string().max(255),
        streetAddress: Yup.string().max(255),
        appartement: Yup.string().max(255),
        city: Yup.string().max(255),
        province: Yup.string().max(255),
        country: Yup.string().max(255),
        postalCode: Yup.string().max(255)
      })}
      onSubmit={async (values, {
        resetForm,
        setErrors,
        setStatus,
        setSubmitting
      }) => {
        try {
          console.log('business on submit', business)
          if(business.businessId =='' ){
            await dispatch(addBusiness(values));
          } else {
            values['businessId'] = business.businessId
            await dispatch(updateBusiness(values));
          }
          // NOTE: Make API request
          resetForm();
          setStatus({ success: true });
          setSubmitting(false);
          enqueueSnackbar('business saved', {
            variant: 'success',
            action: <Button>Ok</Button>
          });
        } catch (err) {
          console.error(err);
          setStatus({ success: false });
          setErrors({ submit: err.message });
          setSubmitting(false);
        }
      }}
    >
      {({
        errors,
        handleBlur,
        handleChange,
        handleSubmit,
        isSubmitting,
        touched,
        values
      }) => (
        <form
          className={clsx(classes.root, className)}
          onSubmit={handleSubmit}
          {...rest}
        >
          <Card>
            <CardContent>
              <Grid
                container
                spacing={3}
              >
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.name && errors.name)}
                    fullWidth
                    helperText={touched.name && errors.name}
                    label="Business name"
                    name="name"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    required
                    value={values.name}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.description && errors.description)}
                    fullWidth
                    helperText={touched.description && errors.description}
                    label="Description"
                    name="description"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    required
                    value={values.description}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.country && errors.country)}
                    fullWidth
                    helperText={touched.country && errors.country}
                    label="Country"
                    name="country"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.country}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.province && errors.province)}
                    fullWidth
                    helperText={touched.province && errors.province}
                    label="State/Region"
                    name="province"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.province}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.city && errors.city)}
                    fullWidth
                    helperText={touched.city && errors.city}
                    label="City"
                    name="city"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.city}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.streetAddress && errors.streetAddress)}
                    fullWidth
                    helperText={touched.streetAddress && errors.streetAddress}
                    label="Street Address"
                    name="streetAddress"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.streetAddress}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.appartement && errors.appartement)}
                    fullWidth
                    helperText={touched.appartement && errors.appartement}
                    label="Appartment Suite"
                    name="appartement"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.appartement}
                    variant="outlined"
                  />
                </Grid>
                <Grid
                  item
                  md={6}
                  xs={12}
                >
                  <TextField
                    error={Boolean(touched.postalCode && errors.postalCode)}
                    fullWidth
                    helperText={touched.postalCode && errors.postalCode}
                    label="Postal Code"
                    name="postalCode"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.postalCode}
                    variant="outlined"
                  />
                </Grid>
                <Grid item />
              </Grid>
              <Box mt={2}>
                <Button
                  variant="contained"
                  color="secondary"
                  type="submit"
                  disabled={isSubmitting}
                >
                  Save business
                </Button>
              </Box>
            </CardContent>
          </Card>
        </form>
      )}
    </Formik>
  );
};

BusinessEditForm.propTypes = {
  className: PropTypes.string,
  // @ts-ignore
  business: PropTypes.object.isRequired
};

export default BusinessEditForm;
