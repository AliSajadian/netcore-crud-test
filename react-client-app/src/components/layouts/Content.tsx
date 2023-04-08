import { FC, ReactElement, ReactNode } from "react";
import { Box, Card } from "@mui/material";

interface ContentProps {
    children: ReactNode;
}

export const Content: FC<ContentProps> = ({ children }): ReactElement => {
  var mnuHeaderHeight = document.getElementById("sidebarMnuHeader")?.clientHeight
  var footerHeight = document.getElementById("footer")?.clientHeight
  const height: string = 
    'calc(100% - ' 
    + (mnuHeaderHeight? mnuHeaderHeight : 0 + (footerHeight? footerHeight : 0)).toString() 
    + ')'
  return (
    <Box
      sx={{
          minHeight: height,
          maxWidth: "100vw",
          mb:2,
          flexGrow: 1,
          backgroundColor: 'inherit'
      }}
    >
      <Card
        sx={{
          display:'flex',
          justifyContent:'center',
          mt:2,
          mx:'auto', 
          height:'100%', 
          width:{xs:'97%', sm:'93%', md:'90%', lg:'90%', xl:'85%'}, 
          boxShadow:3,
          backgroundColor:'inherit'
        }}
      >
        {children}
      </Card>
    </Box>
  )
}

