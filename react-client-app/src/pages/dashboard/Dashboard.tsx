import { FC, ReactElement } from 'react';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import { Typography } from '@mui/material';

const Dashboard: FC = (): ReactElement => {
  return (
    <Box sx={{ flexGrow: 1, p: 2}} data-testid="dashboard-box">
      <Grid  sx={{display:'flex', flexDirection:'row', justifyContent:'center'}}>
        <Grid item xs={10} sm={6} md={6} lg={3}>
          <Typography fontSize={25} fontWeight='bold' >
            THis is React Client App for CSharp Crud Test
          </Typography>
        </Grid>
      </Grid>
    </Box>
  );
}

export default Dashboard;