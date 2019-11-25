import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faTimes, faInfoCircle } from '@fortawesome/free-solid-svg-icons'
import { Tooltip, Toast, ToastBody, ToastHeader } from 'reactstrap';
import moment from 'moment';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);

    this.state = {
      hubConnection: null,
      healthStatuses: [],
      tooltipOpen: [],
      showToast: false,
      updateUrl: ''
    }
  }
  
  componentWillMount = () => {
    this.fetchLatestStatus().then(() => {
      console.log('got latest health status');
    });
  }

  async fetchLatestStatus() {
    const response = await fetch('health/latest');
    const healthStatuses = await response.json();
    for(var i = 0; i < healthStatuses.length; i++) {
      this.updateHealth(healthStatuses[i].success, healthStatuses[i].url, healthStatuses[i].timestamp);
      var tooltipOpen = this.state.tooltipOpen;
      tooltipOpen.push(false);
      this.setState({tooltipOpen});
    }
  }

  componentDidMount = () => {
    const hubConnection = new HubConnectionBuilder().withUrl('/healthhub').build();
    this.setState({hubConnection}, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log('connection started'))
        .catch(err => console.log('Error while establishing connection: ' + err));

      this.state.hubConnection
        .on('ReceiveHealth', (timestamp, success, statusCode, url, elapsedMilliseconds) => {
          this.updateHealth(success, url, timestamp);
        })
    });
  }

  toggle = (index) => this.setTooltipOpen(!this.state.tooltipOpen[index], index);

  setTooltipOpen = (open, index) => {
    var tooltipOpen = this.state.tooltipOpen;
    tooltipOpen[index] = open
    this.setState({tooltipOpen});
  } 

  updateHealth = (success, url, timestamp) => {
    var exists = false
    var healthStatuses = this.state.healthStatuses;
    for (var i = 0; i < healthStatuses.length; i++) {
      if (healthStatuses[i].url === url) {
        healthStatuses[i] = {
          url: url,
          success: success, 
          timestamp: timestamp
        };
        exists = true;
      }
    }
    if (!exists) {
      healthStatuses.push({url: url, success: success, timestamp: timestamp});
    }

    this.setState(healthStatuses);
  }

  render () {
    return (
      <div>
        <AllOperational health={this.state.healthStatuses}></AllOperational>
        <ul style={{paddingInlineStart: 0}}>
        {this.state.healthStatuses.map((health, index) => 
          <li className="list-group-item d-flex justify-content-between align-items-center">
            <span>{health.url}
            <FontAwesomeIcon style={{marginLeft: 5 + 'px'}}icon={faInfoCircle} id={"TooltipExample" + index}></FontAwesomeIcon>
            <Tooltip placement="right" isOpen={this.state.tooltipOpen[index]} target={"TooltipExample" + index} toggle={() => this.toggle(index)}>
              Last update: {moment.utc(health.timestamp).local().format('YYYY-MM-DD HH:mm')}
            </Tooltip>
            </span>
            <EndpointOperational success={health.success}></EndpointOperational>
          </li>
          )}
        </ul>
      </div>
    );
  }
}

function EndpointOperational(props) {
  if (props.success) {
    return <span className="text-success"><FontAwesomeIcon icon={faCheck}></FontAwesomeIcon> Operational</span>
  } else {
    return <span className="text-danger"><FontAwesomeIcon icon={faTimes}></FontAwesomeIcon> Not operational</span>
  }
}

function AllOperational(props) {
  var operational = true;
  for (var i = 0; i < props.health.length; i++) {
    if (props.health[i].success === false) {
      operational = false;
      break;
    }
  }

  if (props.health.length > 0)
  {
    if (operational === true) {
      return <div className="alert alert-success" role="alert">All systems operational!</div>
    } else {
      return <div className="alert alert-danger" role="alert">One or more of the systems are experiencing issues</div>
  
    }
  } else {
    return <div></div>
  }
}