import React, { useState, useEffect } from 'react';
import type { FC, ChangeEvent } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import clsx from 'clsx';
import numeral from 'numeral';
import axios from 'axios';
import General from './General';
import Images from './Images';
import PropTypes from 'prop-types';
import PerfectScrollbar from 'react-perfect-scrollbar';
import { getBusinesses } from 'src/slices/business';

import {
  Avatar,
  Box,
  Button,
  Card,
  Checkbox,
  Divider,
  IconButton,
  InputAdornment,
  Link,
  SvgIcon,
  Tab,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  Tabs,
  TextField,
  Typography,
  makeStyles
} from '@material-ui/core';
import {
  Edit as EditIcon,
  Trash as DeleteIcon,
  Search as SearchIcon
} from 'react-feather';
import type { Theme } from 'src/theme';
import getInitials from 'src/utils/getInitials';
import type { Business } from 'src/types/business';
import { useDispatch, useSelector } from 'src/store';
import { deleteBusiness } from 'src/slices/business';

interface ResultsProps {
  className?: string;
  deleted: any;
  businesses: Business[];
}

type Sort =
  | 'updatedAt|desc'
  | 'updatedAt|asc'
  | 'orders|desc'
  | 'orders|asc';

interface SortOption {
  value: Sort,
  label: string
};

const tabs = [
  {
    value: 'generalInfo',
    label: 'General Info'
  },
  {
    value: 'image',
    label: 'Image'
  }
];

const sortOptions: SortOption[] = [
  {
    value: 'updatedAt|desc',
    label: 'Last update (newest first)'
  },
  {
    value: 'updatedAt|asc',
    label: 'Last update (oldest first)'
  },
  {
    value: 'orders|desc',
    label: 'Total orders (high to low)'
  },
  {
    value: 'orders|asc',
    label: 'Total orders (low to high)'
  }
];

const applyFilters = (businesses: Business[], query: string, filters: any): Business[] => {
  return businesses.filter((business) => {
    let matches = true;

    if (query) {
      const properties = ['email', 'name'];
      let containsQuery = false;

      properties.forEach((property) => {
        if (business[property].toLowerCase().includes(query.toLowerCase())) {
          containsQuery = true;
        }
      });

      if (!containsQuery) {
        matches = false;
      }
    }

    Object.keys(filters).forEach((key) => {
      const value = filters[key];

      if (value && business[key] !== value) {
        matches = false;
      }
    });

    return matches;
  });
};

const applyPagination = (businesses: Business[], page: number, limit: number): Business[] => {
  return businesses.slice(page * limit, page * limit + limit);
};

const descendingComparator = (a: Business, b: Business, orderBy: string): number => {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }

  if (b[orderBy] > a[orderBy]) {
    return 1;
  }

  return 0;
};

const getComparator = (order: 'asc' | 'desc', orderBy: string) => {
  return order === 'desc'
    ? (a: Business, b: Business) => descendingComparator(a, b, orderBy)
    : (a: Business, b: Business) => -descendingComparator(a, b, orderBy);
};

const applySort = (businesses: Business[], sort: Sort): Business[] => {
  const [orderBy, order] = sort.split('|') as [string, 'asc' | 'desc'];
  const comparator = getComparator(order, orderBy);
  const stabilizedThis = businesses.map((el, index) => [el, index]);

  stabilizedThis.sort((a, b) => {
    // @ts-ignore
    const order = comparator(a[0], b[0]);

    if (order !== 0) return order;

    // @ts-ignore
    return a[1] - b[1];
  });

  // @ts-ignore
  return stabilizedThis.map((el) => el[0]);
};

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  queryField: {
    width: 500
  },
  bulkOperations: {
    position: 'relative'
  },
  bulkActions: {
    paddingLeft: 4,
    paddingRight: 4,
    marginTop: 6,
    position: 'absolute',
    width: '100%',
    zIndex: 2,
    backgroundColor: theme.palette.background.default
  },
  bulkAction: {
    marginLeft: theme.spacing(2)
  },
  avatar: {
    height: 42,
    width: 42,
    marginRight: theme.spacing(1)
  }
}));

const Results: FC<ResultsProps> = ({
  className,
  businesses,
  deleted,
  ...rest
}) => {
  const classes = useStyles();
  const [currentTab, setCurrentTab] = useState<string>('generalInfo');
  const [selectedBusinesss, setSelectedBusinesss] = useState<string[]>([]);
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(10);
  const [query, setQuery] = useState<string>('');
  const [sort, setSort] = useState<Sort>(sortOptions[0].value);
  const [filters, setFilters] = useState<any>({
    hasAcceptedMarketing: null,
    isProspect: null,
    isReturning: null
  });
  const { response } = useSelector((state) => state.businesses);
  const dispatch = useDispatch();
  const refreshBusinessList = () => {
    dispatch(getBusinesses());
  }
  useEffect(() => {
    dispatch(getBusinesses());
  }, [dispatch]);
  const handleTabsChange = (event: ChangeEvent<{}>, value: string): void => {
    const updatedFilters = {
      ...filters,
      hasAcceptedMarketing: null,
      isProspect: null,
      isReturning: null
    };

    if (value !== 'all') {
      updatedFilters[value] = true;
    }

    setFilters(updatedFilters);
    setSelectedBusinesss([]);
    setCurrentTab(value);
  };

  const handleQueryChange = (event: ChangeEvent<HTMLInputElement>): void => {
    event.persist();
    setQuery(event.target.value);
  };

  const handleSortChange = (event: ChangeEvent<HTMLInputElement>): void => {
    event.persist();
    setSort(event.target.value as Sort);
  };

  const handleSelectAllBusinesss = (event: ChangeEvent<HTMLInputElement>): void => {
    setSelectedBusinesss(event.target.checked
      ? businesses.map((business) => business.businessId)
      : []);
  };

  const handleSelectOneBusiness = (event: ChangeEvent<HTMLInputElement>, businessId: string): void => {
    if (!selectedBusinesss.includes(businessId)) {
      setSelectedBusinesss((prevSelected) => [...prevSelected, businessId]);
    } else {
      setSelectedBusinesss((prevSelected) => prevSelected.filter((id) => id !== businessId));
    }
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };
  const deleteBusinessDetails = async(id) =>{ 
    await dispatch(deleteBusiness(id));
          // alert('Record deleted successfully!!');  
          deleted();
        }
  const filteredBusinesss = applyFilters(businesses, query, filters);
  const sortedBusinesss = applySort(filteredBusinesss, sort);
  const paginatedBusinesss = applyPagination(sortedBusinesss, page, limit);
  const enableBulkOperations = selectedBusinesss.length > 0;
  const selectedSomeBusinesss = selectedBusinesss.length > 0 && selectedBusinesss.length < businesses.length;
  const selectedAllBusinesss = selectedBusinesss.length === businesses.length;

  return (
    <Card
      className={clsx(classes.root, className)}
      {...rest}
    >
      <Tabs
        onChange={handleTabsChange}
        scrollButtons="auto"
        textColor="secondary"
        value={currentTab}
        variant="scrollable"
      >
        {tabs.map((tab) => (
          <Tab
            key={tab.value}
            value={tab.value}
            label={tab.label}
          />
        ))}
      </Tabs>
      <Divider />
      {currentTab === 'generalInfo' && <General businesses={businesses} deleted={refreshBusinessList}/>}
        {currentTab === 'image' && <Images />}
      
    </Card>
  );
};

Results.propTypes = {
  className: PropTypes.string,
  businesses: PropTypes.array.isRequired,
  deleted: PropTypes.func
};

Results.defaultProps = {
  businesses: []
};

export default Results;
