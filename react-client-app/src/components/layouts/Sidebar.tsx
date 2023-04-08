import { 
  FC, 
  ReactElement 
} from "react";
import { useNavigate } from "react-router-dom";
import { 
  Sidebar, 
  Menu, 
  MenuItem, 
  SubMenu, 
  useProSidebar 
} from "react-pro-sidebar";
import {
  MenuOutlined, 
  HomeOutlined, 
  Person,
  // InfoOutlined, 
  FoodBankOutlined, 
  SearchOutlined,
  RecommendOutlined,
} from "@mui/icons-material";
import { 
  Box, 
  Typography, 
  useTheme
} from "@mui/material";
import { SidebarFooter } from "./SidebarFooter";
import { Badge } from "../utilities/Badge";
import { 
  useSidebar, 
  useSidebarSelectedMenuTitleContext,
  useTemplateDirectionContext,
} from "../../hooks";


export const SideBar: FC = (): ReactElement => {
  const theme = useTheme();
  const navigate = useNavigate();
  const { collapsed } = useProSidebar();
  const { 
    toggle,
    menuItemStyles
  } = useSidebar();
  const { setMenuTitle } = useSidebarSelectedMenuTitleContext();
  const { rtl } = useTemplateDirectionContext()

  const menuItemMouseUpHandler = (mnuTitle: string) => {
    setMenuTitle(mnuTitle)
  }
  return(
      <Sidebar 
        rtl={false} 
        breakPoint="sm"
        transitionDuration={800} 
        style={{ height: "100vh" }}
        backgroundColor={theme.palette.background.paper}
        dir={rtl ? 'rtl' : 'ltr'}
        rootStyles={{
          color: 'inherit'
        }}
      >
        <Menu>
          <MenuItem 
            id="sidebarMnuHeader"
            style={{ 
              textAlign: "center", 
              height: 68, 
              marginTop: 0,
              boxShadow: '0 2px 4px 0 rgba(0, 0, 0, 0.4)',
            }}
            icon={<MenuOutlined 
                    sx={{color: 'inherit'}}
                  />}
            onClick={() => {
              toggle();
            }}
          >
            {" "}
            <Typography 
              sx={{textAlign:"center", fontWeight:"bold", my:"1rem", color: 'inherit'
            }} 
              variant="h5"
            >
              Client App
            </Typography>
          </MenuItem>
        </Menu>
            <Box sx={{ p: '0 24px', mb: '8px', mt: '8px' }}>
              <Typography
                variant="body2"
                fontWeight={600}
                style={{ opacity: collapsed ? 0 : 0.7, letterSpacing: '0.5px' }}
              >
                General
              </Typography>
            </Box>
        <Menu  menuItemStyles={menuItemStyles}>
          <MenuItem 
            icon={<HomeOutlined />} 
            onClick={() => (navigate('/', { replace: true }))} 
            onMouseUp={() => menuItemMouseUpHandler('Home')}
          >
            Home
          </MenuItem>
          {/* <SubMenu icon={<InfoOutlined />} label="BaseInfo">

          </SubMenu> */}
          <MenuItem 
            icon={<Person />} 
            onClick={() => navigate('baseinfo/customer', { replace: true })}
            onMouseUp={() => menuItemMouseUpHandler('Customer')}
          >
            Customer
          </MenuItem>   

          <Box sx={{ py: '0', px:'24px', mb: '8px', mt: '32px' }}>
              <Typography
                variant="body2"
                fontWeight={600}
                style={{ opacity: collapsed ? 0 : 0.7, letterSpacing: '0.5px' }}
              >
                Extra
              </Typography>
            </Box>

            <Menu menuItemStyles={menuItemStyles}>
              <MenuItem icon={<FoodBankOutlined />} suffix={<Badge variant="success">New</Badge>}>
                New Customers
              </MenuItem>
              <SubMenu icon={<SearchOutlined />} label="User Survey">
                <MenuItem>January</MenuItem>
                <MenuItem>February</MenuItem>
                <MenuItem>March</MenuItem>
              </SubMenu>
              <MenuItem disabled icon={<RecommendOutlined />}>
                Examples
              </MenuItem>
            </Menu>
        </Menu>
        <SidebarFooter collapsed={collapsed}/>
      </Sidebar>
  )
}